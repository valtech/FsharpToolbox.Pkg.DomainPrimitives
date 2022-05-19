module FsharpToolbox.Pkg.DomainPrimitives.Test.PersonName

open NUnit.Framework

open FsharpToolbox.Pkg.DomainPrimitives.Strings
open FsharpToolbox.Pkg.DomainPrimitives.Strings.Validators
open FsharpToolbox.Pkg.DomainPrimitives.Test.Util

[<SetUp>]
let Setup () =
    ()

[<TestFixture>]
type PersonNameValidationTests () =

  [<TestCase("Anna")>]
  [<TestCase("anna")>]
  [<TestCase("Bo-Arne")>]
  [<TestCase("Chloë")>]
  [<TestCase("Anna ")>]
  [<TestCase("Sven Erik")>]
  [<TestCase("B.")>]
  [<TestCase("1A")>]
  [<TestCase("123")>]
  [<TestCase("ÆØÅæøå")>]    // Example from #2891
  [<TestCase("Türkçe")>]    // Turkish
  [<TestCase("讠钅饣纟门")>]  // Chinese
  [<TestCase("Ελληνικά")>]  // Greek
  [<TestCase("русский")>]   // Russian
  member this.``Valid names`` (name) =
    Assert.IsTrue(isOk (checkValidPersonName name))

  [<TestCase("")>]
  [<TestCase(" ")>]
  [<TestCase("Hans-%")>]
  [<TestCase("Sven!")>]
  [<TestCase("Sven?")>]
  [<TestCase("Sven:")>]
  member this.``Invalid names`` (name) =
    Assert.IsTrue(isError (checkValidPersonName name))

[<TestFixture>]
type PersonNameCreationTests () =

  [<TestCase("Anna")>]
  member this.``Valid names return Ok with a PersonName containing the name value`` (name) =
    match PersonName.create  name with
    | Ok n -> Assert.True(PersonName.value n = name)
    | Error msg -> Assert.Fail(msg)

  [<TestCase("")>]
  member this.``Empty name returns Error`` (name) =
    match PersonName.create name with
    | Ok _name -> Assert.Fail("should not succeed")
    | Error msg -> Assert.True(msg.Contains("cannot be empty"))

  [<TestCase("Hans-%")>]
  member this.``Invalid name returns Error`` (name) =
    match PersonName.create name with
    | Ok _name -> Assert.Fail("should not succeed")
    | Error msg ->
      Assert.True(msg.Contains("does not contain a valid name"))
      Assert.True(msg.Contains(name), "error msg must contain the invalid name")
