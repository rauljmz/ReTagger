# ReTagger
## Usage

This .Net Core Console application applies the changes defined in an update file to the tags of all the music files found in a folder.

The format of the file is like Tag Update files in [Minimserver](http://minimserver.com/ug-other.html#Tag%20update%20file).

The application can be invoked as follows

```dotnet ReTagger.dll <updatefile> <folder> [-Filter <mask>] [-Simulate]```

The Filter option allows to specify a file mask. Only files matching that mask will be modified. The default is `*.flac`

The Simulate flag, will run the program, provide feedback, but no changes will be made to the files. This is useful to do a check what the effect of running the update file would be.

## Compilation

The solution has been created using Visual Studio 2017. It has a dependency on a Nuget package (taglib-sharp) that has been modified and should **not** be obtained from the official Nuget Repository.

The code can be obtained from my fork in [GitHub](https://github.com/rauljmz/taglib-sharp/tree/netstandard14). 

