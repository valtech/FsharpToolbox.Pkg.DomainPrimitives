module FsharpToolbox.Pkg.DomainPrimitives.Test.Dates

open FsharpToolbox.Pkg.DomainPrimitives.Dates
open NUnit.Framework
open FsharpToolbox.Pkg.DomainPrimitives.Test.Util
open FsharpToolbox.Pkg.DomainPrimitives.Dates.Validators

[<SetUp>]
let Setup () =
    ()

[<TestFixture>]
type DatesValidationTests () =
  [<TestCase("2020-01-20")>]
  [<TestCase("2010-12-20")>]
  [<TestCase("1992-03-04")>]
  [<TestCase("2020-01-20T12:10:30Z")>]
  [<TestCase("2010-12-20T12:10:30Z")>]
  [<TestCase("1992-03-04T12:10:30Z")>]
  member this.``Create Swedish dates`` (dateString) =
    dateString
    |> SwedishDate.create
    |> isOk
    |> Assert.True

  [<TestCase("20200120")>]
  [<TestCase("2010-1220")>]
  [<TestCase("1992-33-04")>]
  [<TestCase("1992-13-04")>]
  [<TestCase("1992-12-44")>]
  [<TestCase("1992-12-twelve")>]
  member this.``Fail creating Swedish dates`` (dateString) =
    dateString
    |> SwedishDate.create
    |> isError
    |> Assert.True

  // Test format
  [<TestCase("2020-03-26T13:00:00+01:00", 26)>]
  [<TestCase("2020-03-26T13:00:00Z", 26)>]
  [<TestCase("2020-03-26T13:00:00+00:00", 26)>]
  // Test converts to Swedish time zone
  [<TestCase("2020-03-26T23:30:00.000+00:00", 27)>]
  [<TestCase("2020-03-26T00:30:00.000+02:00", 25)>]
  member  _.``Can parse date time offset``(input: string, expectedDayOfMonth: int) =
    let result = SwedishDate.create input
    match result with
      | Error e -> $"Expected OK. Error: %A{e}" |> Assert.Fail
      | Ok v -> Assert.AreEqual(expectedDayOfMonth, v.Value.Day)

  [<Test>]
  member _.``Should return false when validFrom is today`` () =
    let pastDate =
      System.DateTime.UtcNow.ToString("yyyy-MM-dd")
      |> SwedishDate.create
      |> function | Ok v -> v | Error e -> failwith "should be Ok!"
    let now = SwedishDate.now()
    let showValidFrom = pastDate > now

    Assert.False showValidFrom

  [<TestCase("2020-01-20")>]
  [<TestCase("2010-12-20")>]
  [<TestCase("1992-03-04")>]
  [<TestCase("2020-01-20T12:10:30Z")>]
  [<TestCase("2010-12-20T12:10:30Z")>]
  [<TestCase("1992-03-04T12:10:30Z")>]
  member _.``Should return false when validFrom is in the past`` (pastDate: string) =
    let pastDate =
      pastDate
      |> SwedishDate.create
      |> function | Ok v -> v | Error e -> failwith "should be Ok!"
    let now = SwedishDate.now()
    let showValidFrom = pastDate > now

    Assert.False showValidFrom

  [<TestCase("3020-01-20")>]
  [<TestCase("3010-12-20")>]
  [<TestCase("3992-03-04")>]
  [<TestCase("3020-01-20T12:10:30Z")>]
  [<TestCase("3010-12-20T12:10:30Z")>]
  [<TestCase("3992-03-04T12:10:30Z")>]
  member _.``Should return true when validFrom is in the future`` (futureDate: string) =
    let futureDate =
      futureDate
      |> SwedishDate.create
      |> function | Ok v -> v | Error e -> failwith "should be Ok!"
    let now = SwedishDate.now()
    let showValidFrom = futureDate > now

    Assert.True showValidFrom

