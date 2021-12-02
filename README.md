# advent
My solutions to [Advent of Code](https://adventofcode.com) in [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0) and C# 10.0

# Template
This repository makes use of [sindrekjr's C# template for Advent of Code](https://github.com/sindrekjr/AdventOfCodeBase)

- [Usage](#usage)
  - [Configuration](#configuration)
  - [Running the project](#running-the-project)
- [License](#license)

## Usage
### Configuration
Create `config.json` with the following key/value pairs. If you run the program without adding a `config.json` file, one will be created for you without a cookie field. The program will not be able to fetch puzzle inputs from the web before a valid cookie is added to the configuration. 
```json
{
  "cookie": "session=c0nt3nt",
  "year": 2020,
  "days": [0] 
}
```

`cookie` - Note that `c0nt3nt` must be replaced with a valid cookie value that your browser stores when logging in at adventofcode.com. Instructions on locating your session cookie can be found here: https://github.com/wimglenn/advent-of-code-wim/issues/1

`year` - Specifies which year you wish to output solutions for when running the project. Defaults to the current year if left unspecified.

`days` - Specifies which days you wish to output solutions for when running the project. Defaults to current day if left unspecified and an event is actively running, otherwise defaults to `0`.

The field supports list comprehension syntax and strings, meaning the following notations are valid.
* `"1..4, 10"` - runs day 1, 2, 3, 4, and 10.
* `[1, 3, "5..9", 15]` - runs day 1, 3, 5, 6, 7, 8, 9, and 15.
* `0` - runs all days

### Running the project
Write your code solutions to advent of code within the appropriate day classes in the Solutions folder, and run the project. From the command line you may do as follows.
```
> cd AdventOfCode
> dotnet build
> dotnet run
```
Using `dotnet run` from the root of the repository will also work as long as you specify which project to run by adding `-p AdventOfCode`. Note that your `config.json` must be stored in the location from where you run your project.

## License
[MIT](https://github.com/sindrekjr/AdventOfCodeBase/blob/master/LICENSE.md)
