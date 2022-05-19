module FsharpToolbox.Pkg.DomainPrimitives.Test.Util

let isOk result =
  match result with
  | Ok a -> true
  | Error b -> false

let isError result =
  not (isOk result)
