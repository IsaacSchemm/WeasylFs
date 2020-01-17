namespace WeasylFs

open System

type Submission = {
    rating: string
    link: string
    owner: string
    owner_login: string
    submitid: int
    title: string
    media: Map<string, Media list>
    posted_at: DateTimeOffset
    subtype: string
    ``type``: string
}