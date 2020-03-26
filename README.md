AIR StyleCop Unity Plugin 
![GitHub release (latest by date)](https://img.shields.io/github/v/release/AnImaginedReality/StyleCop.Package)
[![GitHub issues](https://img.shields.io/github/issues/AnImaginedReality/StyleCop.Package)](https://github.com/AnImaginedReality/StyleCop.Package/issues)
=====================
Easy to install and simple to configure and use, the AIR StyleCop package adds StyleCop to the Unity environment.

At AIR code quality matters, and stylistic consistency goes a long way to making the code of our projects more readable and maintainable. StyleCop is a great tool for achieving consistency, unfortunately because it's built to work as a [nuget package](https://www.nuget.org/packages/StyleCop.Analyzers/), and because unity manages and rebuilds its own project files and removes the stylecop reference, this great tool isn't compatible with the unity environment. This unity package addresses that problem, by hooking into unity's asset pipeline and re-creating the necessary project references automatically. 

## What is StyleCop

[StyleCop](https://github.com/DotNetAnalyzers/StyleCopAnalyzers) is a C# source code analyzer that allows you to enforce a set of style and consistency rules. It allows a team to define style standards for code documentation, layout, maintainability, naming, ordering, readability and spacing. These rules integrate with IDE's such as Visual Studio and Rider, and are fully configurable.

## Installing

To install this package into your unity project, add this git repository as a [Package Reference](https://docs.unity3d.com/Manual/upm-git.html) through the Unity Package manager, as described here in the unity package manager [documentation](https://docs.unity3d.com/Manual/upm-ui-giturl.html):
> https://github.com/AnImaginedReality/StyleCop.Package.git

Though not recommended, it is also possible to add the package to the project directly by downloading a release, and extracting the contents into your unity project under the following path:
> __/Packages/com.air.stylecop/*__

## Configuring

This AIR Stylecop Unity Package by default provides AIR's own rulesets and configuration, but using your own is easy. Simply download copies of the provided [stylecop.ruleset][1] and [stylecop.json][2] files as a starting point (or import your own), and place them in your Unity project's **/ProjectSettings/** folder. The next time Unity rebuilds its solution files it will reference these rather than the defaults. You can edit these rule fies using your IDE, or by hand by referring to the relevent documentation.

Documentation for [stylecop.ruleset][1] is available on the StyleCop github [documentation][3] or on Microsoft's Visual studio [documentation][5]. When configuring stylecop.json see the StyleCop github [documentation][4] page also. 

[1]: https://github.com/AnImaginedReality/StyleCop.Package/blob/master/stylecop.ruleset
[2]: https://github.com/AnImaginedReality/StyleCop.Package/blob/master/stylecop.json
[3]: https://github.com/DotNetAnalyzers/StyleCopAnalyzers/tree/master/documentation
[4]: https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/Configuration.md#getting-started-with-stylecopjson
[5]: https://docs.microsoft.com/en-us/visualstudio/code-quality/rule-set-reference?view=vs-2019

To maintain consistent style across teams/projects, we recommend checking stylecop.ruleset and stylecop.json into the project's version control systems. In this way a project's conventions can be implicitly enforced.

## Running the Tests

Run the EditMode tests for this project through the Unity Editor, by adding it as a **"testables"** reference in your Unity project manifest **/Packages/manifest.json**:

```json
{
  "dependencies": {
    "com.air.stylecop": "https://github.com/AnImaginedReality/StyleCopAnalyzers.Package.git",
    ...
  },
  "lock": {
    "com.air.stylecop": {
      "revision": "HEAD",
      "hash": "79e74e5930592254db806df9833bd7402f56ac23"
    }
    ...
  },
  "testables": [ "com.air.stylecop" ]
}

```

## Versioning
We use [SemVer](http://semver.org/) for versioning. For past and present versions available, see the [tags on this repository](https://github.com/AnImaginedReality/StyleCop.Package/tags). 

## License [![GitHub license](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/AnImaginedReality/StyleCop.Package/blob/master/LICENSE)
This project is licensed udner the MIT license, which provides permission to reuse proprietary this software without warranty, and with a copy of the original MIT license included. 

