# Beasts of Bermuda Dedicated Server

[![WindowsGSH](.github/assets/windowsgsh-badge.svg)](https://windowsgsh.com)
[![Status](https://img.shields.io/badge/status-needs_live_test-f59e0b)](#status)
[![Module version](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fraw.githubusercontent.com%2FWindowsGSH%2FWindowsGSH.BoB%2Fmain%2FBoB.mod%2Fmodule.json&query=%24.version&prefix=v&label=module&color=1E8449)](BoB.mod/module.json)
[![Requires WindowsGSH](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fraw.githubusercontent.com%2FWindowsGSH%2FWindowsGSH.BoB%2Fmain%2FBoB.mod%2Fmodule.json%3Fbadge%3Dminimum&query=%24.minimumWindowsGshVersion&prefix=v&label=requires%20WindowsGSH&color=2563EB)](BoB.mod/module.json)
[![Licence](https://img.shields.io/badge/licence-MIT-64748B)](LICENSE.md)

This WindowsGSH module installs, configures, starts, stops, monitors, imports, and backs up a Beasts of Bermuda dedicated server.

## Status

**NEEDS LIVE TEST.** Launch layout and arguments now match the current dedicated-server documentation. Joining, A2S/player counts, all required sockets, and graceful shutdown still need live validation.

## Installation

The module anonymously installs Steam tool `882430` and launches `BeastsOfBermuda\Binaries\Win64\BeastsOfBermudaServer.exe`. Import `BoB.mod`, add a server, install it, configure it, and start it.

### Import an existing server

WindowsGSH can import either a direct dedicated-server installation or a WindowsGSM folder containing `serverfiles`. Preview verifies the documented executable. Launch settings cannot be inferred safely from the large vendor `Game.ini`, so review all defaults before Copy or Adopt. Preview does not modify the source.

## Configuration

WindowsGSH passes the game mode, map, session name, player limit, bind address, game port, and query port using the documented `-Key=value` launch syntax. `Game.ini` at `BeastsOfBermuda\Saved\Config\WindowsServer\Game.ini` remains vendor/user managed, so its extensive gameplay and administration settings are preserved. Additional arguments are trusted raw command-line text.

## Networking

| Purpose | Default | Protocol | Exposure |
| --- | ---: | --- | --- |
| Game | `7777` | TCP and UDP | Public; eligible for opt-in UPnP. |
| Required service | `8888` | TCP and UDP | Public; fixed by the game and eligible for opt-in UPnP. |
| Steam query | `27015` | TCP and UDP | Public discovery; eligible for opt-in UPnP. |

These declarations follow the current game wiki. Confirm actual listening sockets and remote joining in a live test before beta certification.

## Query, console, and administration

Status is process-based until a current A2S response fixture proves reliable player querying. Output redirection is enabled, but interactive stdin commands are not certified. The module does not claim RCON. Advanced administration belongs in the game's `Game.ini`; no generic RCON password or unrelated GSLT field is exposed.

## Files and backups

| Purpose | Path |
| --- | --- |
| Executable | `BeastsOfBermuda\Binaries\Win64\BeastsOfBermudaServer.exe` |
| Main configuration | `BeastsOfBermuda\Saved\Config\WindowsServer\Game.ini` |
| Worlds, configuration, and logs | `BeastsOfBermuda\Saved` |

The complete `BeastsOfBermuda\Saved` tree is backed up.

## Known limitations

- A2S/player counts, console input, and graceful shutdown are not yet live-certified.
- The fixed `8888` port prevents two servers behind one public IP unless the game offers an undocumented override.
- Game.ini settings are deliberately not rewritten by WindowsGSH.
- Protect configuration, backups, and logs; they may contain private server or administrator data.

## Beta verification checklist

- [ ] Fresh-install Steam app `882430` and confirm executable/process identity.
- [ ] Start each documented game mode/map and confirm the session appears and accepts a remote player.
- [ ] Capture listening sockets and test TCP/UDP `7777`, `8888`, and `27015`, including opt-in UPnP.
- [ ] Capture a current A2S response or retain process-only status.
- [ ] Test direct and WindowsGSM-folder import with Copy and Adopt.
- [ ] Test normal Stop, app exit, Windows session ending, PID reattachment, update, Verify Files, crash diagnostics, backup, and restore.

## Support

Report issues at <https://github.com/WindowsGSH/WindowsGSH.BoB> with versions, a redacted support bundle, and relevant output.

## Support development

If you like the work I do and would like to support continued WindowsGSH and module development, you can contribute here:

- [Ko-fi](https://ko-fi.com/shenniko)
- [PayPal](https://paypal.me/shenniko)

## Trust and source

Modules execute with the same Windows permissions as WindowsGSH. Review `BoB.mod/module.json`, the C# source, [SECURITY.md](SECURITY.md), and [THIRD_PARTY_NOTICES.md](THIRD_PARTY_NOTICES.md) before importing an unfamiliar build.
