module FsharpToolbox.Pkg.DomainPrimitives.Test.DateTimes

open FsharpToolbox.Pkg.DomainPrimitives.DateTimes.NodaTime
open FsharpToolbox.Pkg.DomainPrimitives
open NUnit.Framework
open FsharpToolbox.Pkg.DomainPrimitives.Test.Util
open FsharpToolbox.Pkg.FpUtils
open NodaTime

[<TestFixture>]
type DateTimesValidationTests () =
  [<TestCase("2020-09-08T11:40:30Z")>]
  [<TestCase("1992-03-04T12:00:00Z")>]
  [<TestCase("2018-03-04T12:00:00+05:00")>]
  [<TestCase("2020-03-04T12:00:00-05:00")>]
  [<TestCase("1992-03-04T12:00:00.00000Z")>]
  member this.``Create SwedishDateTime from ISO8601 patternText-string `` (dateString) =
    dateString
    |> DateTimes.SwedishDateTime.create
    |> isOk
    |> Assert.True

  [<TestCase("2020-01-20")>]
  [<TestCase("2010-12-20")>]
  [<TestCase("1992/03/04")>]
  [<TestCase("1992-03-04Z")>]
  [<TestCase("1992/03/04Z")>]
  [<TestCase("20200120")>]
  [<TestCase("2010-1220")>]
  [<TestCase("1992-33-04")>]
  [<TestCase("1992-13-04")>]
  [<TestCase("1992-12-44")>]
  [<TestCase("1992-12-twelve")>]
  [<TestCase("1992/03/04Z")>]
  [<TestCase("1992-03-04T32:00:00Z")>]
  [<TestCase("1992-03-04T12:70:00.00000Z")>]
  [<TestCase("1992-03-04T12:30:90.00000Z")>]
  member this.``Fail creating SwedishDateTime from string that is not ISO8691 compatible`` (dateString) =
    dateString
    |> DateTimes.SwedishDateTime.create
    |> isError
    |> Assert.True

  [<TestCase("2020-01-20")>]
  [<TestCase("2010-12-20")>]
  [<TestCase("2010-11-01")>]
  [<TestCase("1992-03-04Z")>]
  [<TestCase("1992-03-04T12:00:00Z")>]
  [<TestCase("1992-03-04T12:00:00.00000Z")>]
  [<TestCase("1992-03-04T12:30:00+01:00")>]
  [<TestCase("1992-03-04T12:00:00-01:30")>]
  member this.``Creating NodaTime.Instant from string`` (dateString) =
    dateString
    |> NodaTime.Instant.create
    |> isOk
    |> Assert.True

  [<TestCase("2020/01-20")>]
  [<TestCase("99999-01-20")>]
  [<TestCase("2010-12/20")>]
  [<TestCase("1992-03-04H")>]
  [<TestCase("1992-03-04G")>]
  [<TestCase("1992-03-04W")>]
  [<TestCase("1992-03-04T-12:00:00Z")>]
  [<TestCase("1992-0j-04T12:00:00.00000Z")>]
  [<TestCase("1992-13-04T12:30:00+01:00")>]
  [<TestCase("1992-03-34T12:00:00-01:30")>]
  member this.``Creating NodaTime.Instant from invalid string fails`` (dateString) =
    dateString
    |> NodaTime.Instant.create
    |> isError
    |> Assert.True

  [<Test>]
  member this.``Converts string to NodaTime.Instant and back to string`` () =
    let expectedResult = "2020-09-08T14:55:00Z"

    let instant =
      expectedResult
      |> Instant.create
      |>> Instant.value

    match instant with
    | Ok instant -> Assert.AreEqual(instant, expectedResult)
    | _ -> Assert.Fail "Expected strings to be equal"

  [<Test>]
  member this.``Converts string to SwedishDateTime and back to ISO8601-string`` () =
    let expectedResult = "2020-09-08T15:55:00Z"
    let input = "2020-09-08T14:55:00+01:00"

    let localDateTime = DateTimes.SwedishDateTime.create input

    match localDateTime with
    | Ok lDateTime -> Assert.AreEqual(DateTimes.SwedishDateTime.toIsoString lDateTime, expectedResult)
    | e -> Assert.Fail (sprintf "Expected strings to be equal: %A" e)
