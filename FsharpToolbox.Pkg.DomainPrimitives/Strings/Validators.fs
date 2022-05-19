module FsharpToolbox.Pkg.DomainPrimitives.Strings.Validators

open System
open System.Text.RegularExpressions

let checkNotEmpty (s : string)  =
  if System.String.IsNullOrWhiteSpace(s)
  then Error "string cannot be empty"
  else Ok s

let stringIsNotNullOrEmpty (s: string) =
  if not (String.IsNullOrEmpty s)
  then Ok s
  else Error "string cannot be null"

// Based on rules described here: https://en.wikipedia.org/wiki/Email_address
let private emailRegex = Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
  + "@"
  + @"(([A-Za-z0-9][A-Za-z0-9\-]*[A-Za-z0-9]|[A-Za-z])[\.])+[A-Za-z0-9][A-Za-z0-9\-]*[A-Za-z]$")

let checkValidEmail (s: string) =
  if String.length s = 0 then
    Error "email cannot be empty"
  else if emailRegex.IsMatch s then
    s |> Ok
  else
    Error (sprintf "string does not contain a valid email: %s" s)

let private letters = "\\w"
let private nameChars = "[\\d\\w\\s,'\\.-]"
let private nameRegex = Regex(sprintf "^%s%s*$" letters nameChars)

let checkValidPersonName (s: string) =
  if String.length s = 0 then
    Error "name cannot be empty"
  else if nameRegex.IsMatch s then
    s |> Ok
  else
    Error (sprintf "string does not contain a valid name: %s" s)

let tryParseGuid (unvalidatedGuid : string) =
  match Guid.TryParse unvalidatedGuid with
  | true, guid -> Ok guid
  | false, _ -> Error (sprintf "string is not a valid Guid: %s" unvalidatedGuid)
