namespace FsharpToolbox.Pkg.DomainPrimitives

open FsharpToolbox.Pkg.DomainPrimitives.Strings
open NUnit.Framework

[<TestFixture>]
type LanguageTest() =
  [<Test>]
  member _.``Support for language code 'sv_SE'``() =
    // Arrange
    // Act
    let result = Language.create "sv_SE"
    // Assert
    match result with
    | Ok actual ->
      Assert.AreEqual("sv_SE", Language.valueIso actual)
      Assert.AreEqual("sv-SE", Language.value actual)
    | Error e -> Assert.Fail(e)

  [<Test>]
  member _.``Support for language code 'sv-SE'``() =
    // Arrange
    // Act
    let result = Language.create "sv-SE"
    // Assert
    match result with
    | Ok actual ->
      Assert.AreEqual("sv_SE", Language.valueIso actual)
      Assert.AreEqual("sv-SE", Language.value actual)
    | Error e -> Assert.Fail(e)

  [<Test>]
  member _.``Support for language code 'en_US'``() =
    // Arrange
    // Act
    let result = Language.create "en_US"
    // Assert
    match result with
    | Ok actual ->
      Assert.AreEqual("en_US", Language.valueIso actual)
      Assert.AreEqual("en-US", Language.value actual)
    | Error e -> Assert.Fail(e)

  [<Test>]
  member _.``Support for language code 'en-US'``() =
    // Arrange
    // Act
    let result = Language.create "en-US"
    // Assert
    match result with
    | Ok actual ->
      Assert.AreEqual("en_US", Language.valueIso actual)
      Assert.AreEqual("en-US", Language.value actual)
    | Error e -> Assert.Fail(e)

  [<Test>]
  member _.``Sets CultureInfo based on language`` () =
    let expected = System.Globalization.CultureInfo "sv-SE"
    let result = Swedish.getCurrentCulture
    Assert.That(result, Is.EqualTo(expected))
