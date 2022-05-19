module FsharpToolbox.Pkg.DomainPrimitives.Dates.Validators

open FsharpToolbox.Pkg.FpUtils
open FsharpToolbox.Pkg.DomainPrimitives.Strings.Validators
open System.Text.RegularExpressions
open FsharpToolbox.Pkg.Logging

type DateFormat =
  | YYYYMMDD

type SwedishDateError =
  | IsNull of string
  | IncorrectFormat of DateFormat
  | NotParsable of string

let private toIsNullSwedishDateError message =
  sprintf "Date string is null, error: %s" message
  |> SwedishDateError.IsNull
  |> Error

let validateDateValueExists dateString =
  stringIsNotNullOrEmpty dateString
  >>! toIsNullSwedishDateError

let dateFormatCheck (dateString : string) =
  match Regex.Match(dateString, "^\d{4}-\d{2}-\d{2}").Success with
  | true ->
    dateString |> Ok
  | false ->
    L.Error(sprintf "DateTime string is not formatted correctly: %s" dateString)
    SwedishDateError.IncorrectFormat YYYYMMDD |> Error

let handleError e =
  match e with
  | SwedishDateError.IncorrectFormat _ -> sprintf "Incorrect Date Format: yyyy-MM-dd"
  | SwedishDateError.IsNull _ -> sprintf "String is empty"
  | NotParsable e -> sprintf "String is not parsable: %s" e
