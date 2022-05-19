namespace FsharpToolbox.Pkg.DomainPrimitives.Numbers

open FsharpToolbox.Pkg.DomainPrimitives.Numbers
open Validators
open FSharpPlus

type PositiveInt = private PositiveInt of int
  with
  member this.Value =
    this |> fun (PositiveInt x) -> x

module PositiveInt =
  let create (i : int) =
    i |> checkPositiveInt
    |>> PositiveInt

  let createFromString (i : string) =
    match i |> tryParseInt with
    | Ok value -> create value
    | Error e -> Error e

  let value (v : PositiveInt) = v.Value

type NonNegativeInt = private NonNegativeInt of int
  with
  member this.Value =
    this |> fun (NonNegativeInt x) -> x

module NonNegativeInt =
  let create (i : int) =
    i |> checkNonNegativeInt
    |>> NonNegativeInt

  let createFromString  (i : string) =
    match i |> tryParseInt with
    | Ok value -> create value
    | Error e -> Error e

  let value (v : NonNegativeInt) = v.Value

type NonNegativeDecimal = private NonNegativeDecimal of decimal
  with
  member this.Value =
    this |> fun (NonNegativeDecimal x) -> x

module NonNegativeDecimal =
  let create (i : decimal) =
    i |> checkNonNegativeDecimal
    |>> NonNegativeDecimal

  let createFromString (i : string) =
    match i |> tryParseDecimal with
    | Ok value -> create value
    | Error e -> Error e

  let value (v : NonNegativeDecimal) = v.Value

type PositiveInt64 = private PositiveInt64 of int64
  with
  member this.Value =
    this |> fun (PositiveInt64 x) -> x

module PositiveInt64 =
  let create (i : int64) =
    i |> checkPositiveInt64
    |>> PositiveInt64

  let createFromString (i : string) =
    match i |> tryParseInt64 with
    | Ok value -> create value
    | Error e -> Error e

  let value (v : PositiveInt64) = v.Value

type NonNegativeInt64 = private NonNegativeInt64 of int64
  with
  member this.Value =
    this |> fun (NonNegativeInt64 x) -> x

module NonNegativeInt64 =
  let create (i : int64) =
    i |> checkNonNegativeInt64
    |>> NonNegativeInt64

  let createFromString (i : string) =
    match i |> tryParseInt64 with
    | Ok value -> create value
    | Error e -> Error e

  let value (v : NonNegativeInt64) = v.Value

type Percent = private Percent of decimal
  with
  member this.Value =
    this |> fun (Percent x) -> x

module Percent =
  let create (i : decimal) =
    i |> checkIsInPercentRange
    |>> Percent

  let value (v : Percent) = v.Value
