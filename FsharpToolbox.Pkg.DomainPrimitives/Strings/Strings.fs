namespace FsharpToolbox.Pkg.DomainPrimitives.Strings

open FsharpToolbox.Pkg.DomainPrimitives.Strings
open Validators
open FsharpToolbox.Pkg.FpUtils
open FSharpPlus

type NonEmptyString = private NonEmptyString of string
  with
  member this.Value =
    this |> fun (NonEmptyString x) -> x
module NonEmptyString =
  let create (s : string) =
    s |> checkNotEmpty
    >>= (NonEmptyString >> Ok)

  let createWithLabel (label: string) (value: string): Result<NonEmptyString, string> =
    value
    |> create
    |> Result.mapError
         (sprintf "Could not create NonEmptyString for %s, reason: %s" label)

  let value (v : NonEmptyString ) = v.Value

type Email = private Email of string
  with
  member this.Value =
    this |> fun (Email x) -> x
module Email =
  let create (s: string) =
    s
    |> checkNotEmpty
    |>> (String.trimWhiteSpaces >> String.toLower)
    >>= checkValidEmail
    >>= (Email >> Ok)

  let value (v : Email) = v.Value

type PersonName = private PersonName of string
  with
  member this.Value =
    this |> fun (PersonName x) -> x
module PersonName =
  let create (s: string) =
    s |> checkValidPersonName
    >>= (PersonName >> Ok)

  let value (v : PersonName) = v.Value

type Language =
  | Swedish
  | English
  with
  member this.Value =
    this
    |> function
      | Swedish -> "sv-SE"
      | English -> "en-US"
  member this.ValueIso =
    this
    |> function
      | Swedish -> "sv_SE"
      | English -> "en_US"
  member this.getCurrentCulture =
    System.Globalization.CultureInfo this.Value

module Language =
  let toValidLanguage = function
    | "sv-se" | "sv_se" | "sv" -> Ok Swedish
    | "en-us" | "en_us" | "en" -> Ok English
    | language -> Error (sprintf "Language: %s is not implemented" language)

  let create unvalidatedLanguage =
    unvalidatedLanguage
    |> String.toLower
    |> toValidLanguage

  let value (v : Language) = v.Value

  let valueIso (v : Language) = v.ValueIso

