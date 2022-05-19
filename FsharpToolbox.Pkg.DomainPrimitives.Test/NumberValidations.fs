module FsharpToolbox.Pkg.DomainPrimitives.Test.NumberValidations

open NUnit.Framework
open FsharpToolbox.Pkg.DomainPrimitives.Test.Util
open FsharpToolbox.Pkg.DomainPrimitives.Numbers.Validators

[<SetUp>]
let Setup () =
    ()

[<TestFixture>]
type NumbersValidationTests () =

  let failCheckIsInRange i =
    i
    |> checkIsInPercentRange
    |> isError
    |> Assert.True

  let OkCheckIsInRange i =
    i
    |> checkIsInPercentRange
    |> isOk
    |> Assert.True

  [<Test>]
  member this.``Fail checkIsInPercentRange`` () =
    System.Decimal.MinValue
    |> failCheckIsInRange

    System.Decimal.MaxValue
    |> failCheckIsInRange

    1.1m
    |> failCheckIsInRange

  [<Test>]
  member this.``Ok checkIsInPercentRange`` () =
    1m
    |> OkCheckIsInRange

    0.5m
    |> OkCheckIsInRange

    0m
    |> OkCheckIsInRange

  [<TestCase(0)>]
  [<TestCase(1)>]
  [<TestCase(-1)>]
  [<TestCase(12451)>]
  [<TestCase(-12451)>]
  [<TestCase(System.Int32.MinValue)>]
  [<TestCase(System.Int32.MaxValue)>]
  [<TestCase("-2")>]
  [<TestCase("5")>]
  member this.``tryParseInt`` (numberString) =
    numberString
    |> string
    |> tryParseInt
    |> isOk
    |> Assert.True

  [<TestCase("aString")>]
  member this.``Fail tryParseInt strings`` (number) =
    number
    |> tryParseInt
    |> isError
    |> Assert.True

  [<TestCase(System.Int64.MinValue)>]
  [<TestCase(System.Int64.MaxValue)>]
  [<TestCase("3.1415926535")>]
  member this.``Fail tryParseInt numbers`` (number) =
    number
    |> string
    |> tryParseInt
    |> isError
    |> Assert.True

  [<TestCase(0)>]
  [<TestCase(1)>]
  [<TestCase(-1)>]
  [<TestCase(12451)>]
  [<TestCase(-12451)>]
  [<TestCase(System.Int64.MinValue)>]
  [<TestCase(System.Int64.MaxValue)>]
  [<TestCase("-3")>]
  [<TestCase("39")>]
  member this.``tryParseInt64`` (numberString) =
    numberString
    |> string
    |> tryParseInt64
    |> isOk
    |> Assert.True

  [<TestCase("aString")>]
  member this.``Fail tryParseInt64 strings`` (number) =
    number
    |> tryParseInt64
    |> isError
    |> Assert.True

  [<TestCase("3.1415926535")>]
  member this.``Fail tryParseInt64 numbers`` (number) =
    number
    |> string
    |> tryParseInt64
    |> isError
    |> Assert.True

  [<TestCase(1)>]
  [<TestCase(13)>]
  [<TestCase(12451)>]
  [<TestCase(System.Int32.MaxValue)>]
  [<TestCase("42")>]
  member this.``checkPositiveInt`` (number) =
    number
    |> checkPositiveInt
    |> isOk
    |> Assert.True

  [<TestCase(0)>]
  [<TestCase(-1)>]
  [<TestCase(-13)>]
  [<TestCase(-12451)>]
  [<TestCase(System.Int32.MinValue)>]
  [<TestCase("-1")>]
  member this.``Fail checkPositiveInt`` (number) =
    number
    |> checkPositiveInt
    |> isError
    |> Assert.True

  [<TestCase(0)>]
  [<TestCase(1)>]
  [<TestCase(13)>]
  [<TestCase(12451)>]
  [<TestCase(System.Int32.MaxValue)>]
  [<TestCase("0")>]
  [<TestCase("2124")>]
  member this.``checkNonNegativeInt`` (number) =
    number
    |> checkNonNegativeInt
    |> isOk
    |> Assert.True

  [<TestCase(-1)>]
  [<TestCase(-13)>]
  [<TestCase(-12451)>]
  [<TestCase("-15")>]
  //[<TestCase(System.Decimal.MinValue)>]
  member this.``Fail checkNonNegativeInt`` (number) =
    number
    |> checkNonNegativeInt
    |> isError
    |> Assert.True

  [<TestCase(0.0)>]
  [<TestCase(1.1)>]
  [<TestCase(13.352)>]
  [<TestCase(12451.4214)>]
  [<TestCase("12451.4214")>]
  //[<TestCase(System.Decimal.MaxValue)>]
  member this.``checkNonNegativeDecimal`` (number) =
    number
    |> checkNonNegativeDecimal
    |> isOk
    |> Assert.True

  [<TestCase(-1)>]
  [<TestCase(-13)>]
  [<TestCase(-12451)>]
  [<TestCase(System.Int32.MinValue)>]
  [<TestCase("-2.412")>]
  member this.``Fail checkNonNegativeDecimal`` (number) =
    number
    |> checkNonNegativeDecimal
    |> isError
    |> Assert.True

  [<TestCase(1)>]
  [<TestCase(13)>]
  [<TestCase(12451)>]
  [<TestCase(System.Int64.MaxValue)>]
  [<TestCase("9223372036854775807")>]
  member this.``checkPositiveInt64`` (number) =
    number
    |> checkPositiveInt64
    |> isOk
    |> Assert.True

  [<TestCase(0)>]
  [<TestCase(-1)>]
  [<TestCase(-13)>]
  [<TestCase(-12451)>]
  [<TestCase(System.Int64.MinValue)>]
  [<TestCase("-224")>]
  member this.``Fail checkPositiveInt64`` (number) =
    number
    |> checkPositiveInt64
    |> isError
    |> Assert.True

  [<TestCase(0)>]
  [<TestCase(1)>]
  [<TestCase(13)>]
  [<TestCase(12451)>]
  [<TestCase(System.Int64.MaxValue)>]
  [<TestCase("0")>]
  [<TestCase("345345342")>]
  member this.``checkNonNegativeInt64`` (number) =
    number
    |> checkNonNegativeInt64
    |> isOk
    |> Assert.True

  [<TestCase(-1)>]
  [<TestCase(-13)>]
  [<TestCase(-12451)>]
  [<TestCase(System.Int64.MinValue)>]
  [<TestCase("-123434234234")>]
  member this.``Fail checkNonNegativeInt64`` (number) =
    number
    |> checkNonNegativeInt64
    |> isError
    |> Assert.True

  [<TestCase(0, 100, 0)>]
  [<TestCase(-3, 4, 1)>]
  [<TestCase(-14, -10, -11)>]
  [<TestCase(100, 200, 150)>]
  [<TestCase(System.Int32.MinValue, System.Int32.MaxValue, 1)>]
  member this.``checkIsInRange`` (min, max, number) =
    number
    |> checkIsInRange min max
    |> isOk
    |> Assert.True

  [<TestCase(4, 100, 1)>]
  [<TestCase(-3, 4, 5)>]
  [<TestCase(-3, 4, -5)>]
  [<TestCase(-13, -10, -15)>]
  [<TestCase(-13, -10, -5)>]
  [<TestCase(System.Int32.MaxValue, System.Int32.MinValue, 1)>]
  member this.``Fail checkIsInRange`` (min, max, number) =
    number
    |> checkIsInRange min max
    |> isError
    |> Assert.True

