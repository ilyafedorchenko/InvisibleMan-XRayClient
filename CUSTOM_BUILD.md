# Windows x64 custom build

This fork keeps the original Invisible Man XRay 3.2.5 interface and makes two targeted changes:

- updates Xray-core to `v26.3.27` (`github.com/xtls/xray-core v1.260327.0`);
- preserves the `flow` value from VLESS links, including an empty value for REALITY + gRPC.

The GitHub Actions workflow builds `XRayCore.dll`, runs a VLESS configuration smoke test, publishes the self-contained Windows x64 application, adds the official Invisible Man TUN v0.3.5 files and current `geoip.dat` / `geosite.dat`, and creates a ZIP with a SHA-256 checksum.

The resulting application is not code-signed. Windows SmartScreen may display an unknown publisher warning.

Trimming is disabled because the application combines WPF and Windows Forms; .NET 7 does not support trimming Windows Forms applications safely.
Consequently, the self-contained main executable is larger than the upstream 3.2.5 executable. Most of the increase is the bundled .NET Desktop Runtime, not Xray-core. This is an intentional compatibility trade-off; do not re-enable `PublishTrimmed` without testing the tray icon, context menu, TUN mode, reconnect and application shutdown on Windows.

## Reproducible build

The build is pinned to Go 1.26.1 and .NET SDK 7.0.410. To build it on GitHub, run the **Build Windows x64** workflow on branch `codex/fix-grpc-reality-xray-26.3.27`. The workflow output is the `InvisibleManXRay-x64-v3.2.5.1-xray26.3.27` artifact containing the ZIP and its SHA-256 checksum.

The workflow uploads an Actions artifact; publishing or replacing assets in a GitHub Release is a separate manual step. The matching release tag is `v3.2.5.1-xray26.3.27`.

Verify a downloaded package on macOS or Linux without relying on the checksum file's line endings:

```sh
expected="$(awk '{print $1}' InvisibleManXRay-x64-v3.2.5.1-xray26.3.27.zip.sha256 | tr -d '\r')"
actual="$(shasum -a 256 InvisibleManXRay-x64-v3.2.5.1-xray26.3.27.zip | awk '{print $1}')"
test "$actual" = "$expected"
```

Before distributing a build, test on Windows x64 that the application starts, imports the REALITY + gRPC VLESS link with an empty `flow`, connects successfully, passes traffic, enables and disables TUN mode, and exits cleanly from the tray menu.
