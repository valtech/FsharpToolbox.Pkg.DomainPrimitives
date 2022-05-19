module FsharpToolbox.Pkg.DomainPrimitives.Test.Email

open NUnit.Framework

open FsharpToolbox.Pkg.TestUtils
open FsharpToolbox.Pkg.DomainPrimitives.Strings
open FsharpToolbox.Pkg.DomainPrimitives.Strings.Validators
open FsharpToolbox.Pkg.DomainPrimitives.Test.Util

[<SetUp>]
let Setup () =
    ()

// The regex in checkValidEmail is based on these rules: https://en.wikipedia.org/wiki/Email_address
// Some rules are not implemented by the regex, so those examples have been commented out here.

[<TestFixture>]
type EmailTest () =

  [<TestCase("simple@example.com")>]
  [<TestCase("very.common@example.com")>]
  [<TestCase("disposable.style.email.with+symbol@example.com")>]
  [<TestCase("other.email-with-hyphen@example.com")>]
  [<TestCase("fully-qualified-domain@example.com")>]
  [<TestCase("user.name+tag+sorting@example.com")>] // may go to user.name@example.com inbox depending on mail server
  [<TestCase("x@example.com")>] // one-letter local-part
  [<TestCase("example-indeed@strange-example.com")>]
  //[<TestCase("admin@mailserver1")>] // local domain name with no TLD, although ICANN highly discourages dotless email addresses[13]
  [<TestCase("example@s.example")>] // see the List of Internet top-level domains
  //[<TestCase("\" \"@example.org")>]  // space between the quotes
  //[<TestCase("\"john..doe\"@example.org")>] // quoted double dot
  [<TestCase("mailhost!username@example.org")>] // bangified host route used for uucp mailers
  [<TestCase("user%example.com@example.org")>]
  member this.``Valid emails according to Wikipedia`` (email) =
    Assert.IsTrue(isOk (checkValidEmail email))

  [<TestCase("Abc.example.com")>] // no @ character
  [<TestCase("A@b@c@example.com")>] // only one @ is allowed outside quotation marks)
  [<TestCase("a\"b(c)d,e:f;g<h>i[j\k]l@example.com")>] // none of the special characters in this local-part are allowed outside quotation marks
  [<TestCase("just\"not\"right@example.com")>] // quoted strings must be dot separated or the only element making up the local-part
  [<TestCase("this is\"not\\allowed@example.com")>] // spaces, quotes, and backslashes may only exist when within quoted strings and preceded by a backslash
  [<TestCase("this\ still\"not\\allowed@example.com")>] // even if escaped (preceded by a backslash), spaces, quotes, and backslashes must still be contained by quotes
  // [<TestCase("1234567890123456789012345678901234567890123456789012345678901234+x@example.com")>] // local part is longer than 64 characters
  [<TestCase("i_like_underscore@but_its_not_allow_in _this_part.example.com")>]
  member this.``Invalid emails according to Wikipedia`` (email) =
    Assert.IsTrue(isError (checkValidEmail email))

  [<Test>]
  member _.``Validates alternating-case email-request and normalizes in lowercase`` () =
    let unvalidatedEmail = "atrain.TESTMAIL@EXAMPLE.com"
    let validatedEmail = Email.create unvalidatedEmail |> Assert.assumeOk

    Assert.That(validatedEmail.Value, Is.EqualTo("atrain.testmail@example.com"))

  [<TestCase(null)>]
  [<TestCase(" ")>]
  [<TestCase("")>]
  [<TestCase(" \n ")>]
  [<TestCase("test%40mail.com")>]
  member _.``Returns Error when string cannot be parsed into valid Email`` (unvalidatedEmail : string) =
    let validatedEmail = Email.create unvalidatedEmail

    match validatedEmail with
    | Error s -> Assert.Pass()
    | Ok email -> Assert.Fail "Expected Error, got Ok %s{email.value}"

  [<TestCase("    user%example.com@example.org      ")>]
  [<TestCase("    user+example.com@example.org")>]
  [<TestCase("user+example.com@example.org      ")>]
  [<TestCase("USER+EXAMPLE.COM@EXAMPLE.ORG      ")>]
  member _.``Returns Ok email when string can be parsed into valid Email`` (unvalidatedEmail : string) =
    let validatedEmail = Email.create unvalidatedEmail

    match validatedEmail with
    | Error s -> Assert.Fail "Expected Ok, got Error %s{s}"
    | Ok email -> Assert.Pass ()
