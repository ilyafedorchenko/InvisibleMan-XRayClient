# Windows x64 custom build

This fork keeps the original Invisible Man XRay 3.2.5 interface and makes two targeted changes:

- updates Xray-core to `v26.3.27` (`github.com/xtls/xray-core v1.260327.0`);
- preserves the `flow` value from VLESS links, including an empty value for REALITY + gRPC.

The GitHub Actions workflow builds `XRayCore.dll`, runs a VLESS configuration smoke test, publishes the self-contained Windows x64 application, adds the official Invisible Man TUN v0.3.5 files and current `geoip.dat` / `geosite.dat`, and creates a ZIP with a SHA-256 checksum.

The resulting application is not code-signed. Windows SmartScreen may display an unknown publisher warning.
