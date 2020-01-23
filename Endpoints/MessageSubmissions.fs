namespace WeasylFs.Endpoints

open System
open WeasylFs
open FSharp.Json
open FSharp.Json.Transforms

module MessageSubmissions =
    type Request() =
        member val Count = Nullable<int>() with get, set
        member val BackTime = Nullable<DateTimeOffset>() with get, set
        member val NextTime = Nullable<DateTimeOffset>() with get, set

        member this.QueryString =
            seq {
                if this.Count.HasValue then
                    yield ("count", this.Count.Value.ToString())
                if this.BackTime.HasValue then
                    yield ("backtime", this.BackTime.Value.ToUnixTimeSeconds().ToString())
                if this.NextTime.HasValue then
                    yield ("nexttime", this.NextTime.Value.ToUnixTimeSeconds().ToString())
            }
            |> Util.BuildQueryString

    type Response = {
        submissions: Submission list
        [<JsonField(Transform=typeof<DateTimeOffsetEpoch>)>]
        backtime: DateTimeOffset option
        [<JsonField(Transform=typeof<DateTimeOffsetEpoch>)>]
        nexttime: DateTimeOffset option
    } with
        member this.backtime_or_null =
            this.backtime
            |> Option.toNullable
        member this.nexttime_or_null =
            this.nexttime
            |> Option.toNullable

    let AsyncExecute credentials (parameters: Request) =
        sprintf "api/messages/submissions?%s" (parameters.QueryString)
        |> Util.CreateRequest credentials
        |> Util.AsyncReadJson<Response>

    let ExecuteAsync credentials parameters =
        AsyncExecute credentials parameters
        |> Async.StartAsTask