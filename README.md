# JDK Version Manager (JVMS.NET) for Windows

JVMS.NET is a .NET Core 9 port of the [JVMS](https://github.com/ystyle/jvms) tool, which helps you manage multiple installations of JDK on a Windows computer.

## Features

- Install multiple JDK versions side by side
- Switch between JDK versions with a single command
- List installed JDK versions
- List available JDK versions for download
- Remove JDK versions easily
- Configure proxy settings for downloads

## Requirements

- Windows operating system
- .NET Core 9 runtime
- Administrator privileges (required to modify environment variables and create symbolic links)

## Installation

1. Download the latest release from the [Releases](https://github.com/juancarlosbaezpozos/Jvms.Net/releases) page
2. Extract the zip file to a location of your choice
3. Open a command prompt or PowerShell window with administrator privileges
4. Navigate to the directory containing `jvms.exe`
5. Run `jvms init` to initialize the tool

## Usage

```
jvms [command] [options]
```

### Available Commands

- `init`: Initialize JVMS.NET (required on first use)
    - Options:
        - `--java_home <path>`: Set the JAVA_HOME location (default: C:\Program Files\jdk)
        - `--originalpath <url>`: Set the JDK download index file URL

- `list` (or `ls`): List installed JDK versions

- `install <version>` (or `i <version>`): Install a specific JDK version
    - Example: `jvms install 17.0.6`

- `switch <version>` (or `s <version>`): Switch to a specific JDK version
    - Example: `jvms switch 17.0.6`

- `remove <version>` (or `rm <version>`): Remove a specific JDK version
    - Example: `jvms remove 17.0.6`

- `rls`: Show available JDK versions for download
    - Options:
        - `-a`: List all available versions (not just the first 10)

- `proxy`: Configure proxy settings
    - Options:
        - `--show`: Show current proxy settings
        - `--set <url>`: Set proxy URL

## Examples

Initialize JVMS.NET:
```
jvms init
```

List available JDK versions for download:
```
jvms rls
```

Install a specific JDK version:
```
jvms install 17.0.6
```

Switch to an installed JDK version:
```
jvms switch 17.0.6
```

List installed JDK versions:
```
jvms ls
```

Remove a JDK version:
```
jvms remove 17.0.6
```

## How it Works

JVMS.NET manages JDK installations by:

1. Downloading and extracting JDK zip files to a managed storage location
2. Creating a symbolic link from the system JAVA_HOME to the desired JDK version
3. Modifying the system PATH environment variable to include JAVA_HOME/bin

This approach allows for quick switching between JDK versions without having to modify environment variables each time.

## License

MIT License

## Credits

This tool is a .NET Core port of the original [JVMS](https://github.com/ystyle/jvms) created by ystyle.
Port to .NET Core by Carlos Báez.