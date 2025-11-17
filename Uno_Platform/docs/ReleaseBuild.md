# Release Build Documentation

## Overview
This document provides instructions for building release versions of the Uno Platform University Mobile App for Android and WebAssembly platforms.

## Prerequisites

### Required Software
- .NET 9.0 SDK or later
- Uno Platform SDK 6.4.24
- Android SDK (for Android builds)
- Java Development Kit (JDK) 11 or later
- Android Studio (optional, for emulator testing)

### Required Workloads
```powershell
dotnet workload install android
dotnet workload install wasm-tools
```

### Environment Variables
- `ANDROID_HOME` - Path to Android SDK
- `JAVA_HOME` - Path to JDK installation

---

## Android Release Build

### Step 1: Generate Signing Keystore

#### Option A: Using keytool (Command Line)
```powershell
keytool -genkeypair -v -storetype PKCS12 -keystore release.keystore -alias uno-platform -keyalg RSA -keysize 2048 -validity 10000
```

**Parameters:**
- `-storetype PKCS12`: Keystore format
- `-keystore release.keystore`: Output keystore file
- `-alias uno-platform`: Key alias
- `-keyalg RSA`: Encryption algorithm
- `-keysize 2048`: Key size
- `-validity 10000`: Validity in days (approximately 27 years)

**You will be prompted for:**
- Keystore password
- Key password (can be same as keystore password)
- Your name and organizational information

#### Option B: Using Android Studio
1. Open Android Studio
2. Build → Generate Signed Bundle / APK
3. Create new keystore
4. Fill in required information
5. Save keystore file

### Step 2: Configure Signing in Project

Create or update `Uno_Platform/Uno_Platform/Uno_Platform.csproj`:

```xml
<PropertyGroup Condition="'$(Configuration)' == 'Release'">
  <AndroidKeyStore>True</AndroidKeyStore>
  <AndroidSigningKeyStore>path/to/release.keystore</AndroidSigningKeyStore>
  <AndroidSigningStorePass>your-keystore-password</AndroidSigningStorePass>
  <AndroidSigningKeyAlias>uno-platform</AndroidSigningKeyAlias>
  <AndroidSigningKeyPass>your-key-password</AndroidSigningKeyPass>
</PropertyGroup>
```

**Security Note:** Never commit passwords to source control. Use environment variables or secure configuration files.

### Step 3: Build Release APK

```powershell
cd Uno_Platform\Uno_Platform
dotnet build -c Release -f net9.0-android
```

**Output Location:**
```
bin/Release/net9.0-android/Uno_Platform-Signed.apk
```

### Step 4: Build Release AAB (Android App Bundle)

```powershell
dotnet publish -c Release -f net9.0-android
```

**Output Location:**
```
bin/Release/net9.0-android/publish/Uno_Platform.aab
```

**AAB vs APK:**
- **AAB (App Bundle):** Required for Google Play Store
- **APK:** Can be installed directly on devices

### Step 5: Verify Build

#### Check APK
```powershell
# Using Android SDK tools
$ANDROID_HOME\build-tools\[version]\aapt dump badging Uno_Platform-Signed.apk
```

#### Check AAB
```powershell
# Using bundletool (download from Google)
java -jar bundletool.jar build-apks --bundle=Uno_Platform.aab --output=app.apks
```

---

## WebAssembly Release Build

### Step 1: Build Release Version

```powershell
cd Uno_Platform\Uno_Platform
dotnet publish -c Release -f net9.0-browserwasm
```

**Output Location:**
```
bin/Release/net9.0-browserwasm/publish/wwwroot/
```

### Step 2: Optimize Build

The release build automatically:
- Minifies JavaScript
- Optimizes WebAssembly binaries
- Compresses assets
- Removes debug symbols

### Step 3: Test Locally

```powershell
# Using .NET HTTP server
dotnet serve -d bin/Release/net9.0-browserwasm/publish/wwwroot
```

Or use any static file server:
- IIS
- Nginx
- Apache
- Azure Static Web Apps
- GitHub Pages

### Step 4: Deploy to Hosting

#### Option A: Azure Static Web Apps
1. Create Azure Static Web App resource
2. Connect to GitHub repository
3. Configure build settings:
   - App location: `Uno_Platform/Uno_Platform/bin/Release/net9.0-browserwasm/publish/wwwroot`
   - Output location: `.`

#### Option B: GitHub Pages
1. Copy `wwwroot` contents to `docs` folder
2. Enable GitHub Pages in repository settings
3. Select `docs` folder as source

#### Option C: Traditional Web Hosting
1. Upload `wwwroot` contents to web server
2. Configure server to serve `index.html` for all routes
3. Ensure MIME types are configured for `.wasm` files

---

## iOS Release Build (macOS Only)

### Prerequisites
- macOS with Xcode installed
- Apple Developer account
- Provisioning profile

### Step 1: Configure Signing

Update `Uno_Platform.csproj`:
```xml
<PropertyGroup Condition="'$(Configuration)' == 'Release'">
  <CodesignKey>iPhone Developer</CodesignKey>
  <ProvisioningType>automatic</ProvisioningType>
</PropertyGroup>
```

### Step 2: Build IPA

```bash
cd Uno_Platform/Uno_Platform
dotnet publish -c Release -f net9.0-ios
```

### Step 3: Archive and Export

1. Open project in Xcode
2. Product → Archive
3. Distribute App
4. Choose distribution method (App Store, Ad Hoc, Enterprise)

---

## Build Configuration

### Debug vs Release

#### Debug Configuration
- Includes debug symbols
- No code optimization
- Larger file size
- Slower performance
- Detailed error messages

#### Release Configuration
- Optimized code
- Smaller file size
- Better performance
- Minified assets
- Production-ready

### Configuration Files

#### launchSettings.json
Located in `Properties/launchSettings.json`:
```json
{
  "profiles": {
    "Android": {
      "commandName": "Project",
      "targetFramework": "net9.0-android"
    },
    "WebAssembly": {
      "commandName": "Project",
      "targetFramework": "net9.0-browserwasm",
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5000"
    }
  }
}
```

---

## Version Management

### Update Version Numbers

Edit `Uno_Platform.csproj`:
```xml
<PropertyGroup>
  <ApplicationDisplayVersion>1.0.0</ApplicationDisplayVersion>
  <ApplicationVersion>1</ApplicationVersion>
</PropertyGroup>
```

### Version Format
- **ApplicationDisplayVersion:** User-visible version (e.g., "1.0.0")
- **ApplicationVersion:** Internal version number (increment for each release)

---

## Signing Configuration Template

See `release/signing-template.json` for a template structure:

```json
{
  "android": {
    "keystore": {
      "path": "path/to/keystore",
      "alias": "uno-platform",
      "storePassword": "${ANDROID_KEYSTORE_PASSWORD}",
      "keyPassword": "${ANDROID_KEY_PASSWORD}"
    }
  },
  "ios": {
    "provisioningProfile": "path/to/profile.mobileprovision",
    "certificate": "path/to/certificate.p12"
  }
}
```

**Note:** Use environment variables for passwords, never hardcode them.

---

## Troubleshooting

### Android Build Issues

#### Issue: "Android SDK not found"
**Solution:**
```powershell
# Set ANDROID_HOME environment variable
$env:ANDROID_HOME = "C:\Users\YourName\AppData\Local\Android\Sdk"
```

#### Issue: "Keystore password incorrect"
**Solution:** Verify keystore password and key alias are correct

#### Issue: "AAB build fails"
**Solution:** Ensure all dependencies are restored:
```powershell
dotnet restore
dotnet workload restore
```

### WebAssembly Build Issues

#### Issue: "WASM files not loading"
**Solution:** Ensure web server is configured with correct MIME types:
- `.wasm` → `application/wasm`
- `.js` → `application/javascript`

#### Issue: "Build is too large"
**Solution:** Enable trimming and AOT compilation (if supported):
```xml
<PropertyGroup>
  <PublishTrimmed>true</PublishTrimmed>
</PropertyGroup>
```

---

## CI/CD Integration

### GitHub Actions Example

```yaml
name: Build and Release

on:
  push:
    tags:
      - 'v*'

jobs:
  build-android:
    runs-on: windows-latest
    steps:
      - uses: actions/checkout@v3
      - uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '9.0.x'
      - run: dotnet workload restore
      - run: dotnet publish -c Release -f net9.0-android
      - uses: actions/upload-artifact@v3
        with:
          name: android-release
          path: bin/Release/net9.0-android/publish/*.aab

  build-wasm:
    runs-on: windows-latest
    steps:
      - uses: actions/checkout@v3
      - uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '9.0.x'
      - run: dotnet workload restore
      - run: dotnet publish -c Release -f net9.0-browserwasm
      - uses: actions/upload-artifact@v3
        with:
          name: wasm-release
          path: bin/Release/net9.0-browserwasm/publish/wwwroot
```

---

## Store Submission

### Google Play Store

1. **Create App Listing**
   - App name, description, screenshots
   - Category and content rating
   - Privacy policy URL

2. **Upload AAB**
   - Go to Play Console → Release → Production
   - Upload `Uno_Platform.aab`
   - Fill in release notes

3. **Complete Store Listing**
   - Add graphics (icon, feature graphic)
   - Set pricing and distribution
   - Submit for review

### Apple App Store

1. **Create App Record**
   - App name, description, keywords
   - Category and content rating
   - Privacy policy URL

2. **Upload IPA**
   - Use Xcode or Application Loader
   - Upload `Uno_Platform.ipa`
   - Fill in version information

3. **Submit for Review**
   - Complete app information
   - Submit for App Store review

---

## Security Considerations

1. **Never commit keystore files** to version control
2. **Use environment variables** for passwords
3. **Enable ProGuard/R8** for Android (code obfuscation)
4. **Review permissions** in AndroidManifest.xml
5. **Use HTTPS** for WebAssembly deployments
6. **Implement certificate pinning** for sensitive apps

---

## Performance Optimization

### Android
- Enable ProGuard/R8
- Use AOT compilation
- Optimize images and assets
- Minimize dependencies

### WebAssembly
- Enable compression (gzip/brotli)
- Use CDN for static assets
- Implement lazy loading
- Optimize bundle size

---

## Maintenance

### Regular Tasks
- Update dependencies monthly
- Test on latest OS versions
- Monitor crash reports
- Update version numbers
- Review and update signing certificates before expiration

