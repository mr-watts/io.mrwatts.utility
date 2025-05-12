Package containing various utilities for use in our Unity projects.

# Table of Contents

[[_TOC_]]

# Installation

## 1. Package

### Private GitLab

Add this scoped registry.

```json
"scopedRegistries": [
    {
      "name": "Mr. Watts UPM Registry",
      "url": "https://gitlab.com/api/v4/projects/27157125/packages/npm/",
      "scopes": [
        "io.mrwatts"
      ]
    }
  ]
```

Add the following dependency in the **manifest.json** file in the "Packages" folder.

_"io.mrwatts.utility": "1.0.0"_

> The version number should be the latest version of the package (unless you want to target an older version on purpose).

### Public GitHub

Add the following dependency in the **manifest.json** file in the "Packages" folder.

_"io.mrwatts.utility": "https://github.com/mr-watts/io.mrwatts.utility.git?path=/unity-utility-package/Assets/Utility#1.0.0"_

> The version number should be the latest version of the package (unless you want to target an older version on purpose).

## 2. Assembly References

Add Assembly reference to `mrwatts.utility.runtime` to your application's assembly definitions (e.g. `Scripts.asmdef`) you want to use this package in. It is not auto-referenced.

# Package Development

To run most CLI commands, you need a couple of environment variables to be set. These can be found in `.env.dist`. Ensure these are loaded in your environment. There are a couple of ways to do that:

1. Export the variables in `.env.dist` manually:
    1. [Using `$env:FOO = 'Bar'`](https://stackoverflow.com/a/714918) (PowerShell only)
    1. Using `SET FOO=Bar` (Windows Command Prompt only)
    1. Using `export FOO=BAR` (Bash and compatible shells only).
        - With Bash, you can also put these in your `.bashrc` to not have to do this every time, if desired.
    1. Prepend the variables in `.env.dist` to the command using `FOO=BAR BAZ=CUX dotnet ...` (Bash and compatible shells only).
1. Copy `.env.dist` to `.env`, fill in the variables to your liking, and use something like [direnv](https://direnv.net/) to automatically load them into your environment. (That way you can use the same configuration for native and container builds.)

## Installing NuGet Packages

After all necessary variables are in your environment, in the project root folder:

```sh
dotnet msbuild -target:PostProcessDotNetPackagesForUnity -restore NuGetDependencies/Unity/
```

After post-processing finishes, you can start or focus the Unity window of your project and let Unity import the dependencies.

## Running Roslyn Analyzers

If you want to check for analyzer errors across the codebase in one go, run:

```
dotnet build
```