# Domain Models

## User

```json
{
	"id": "00000000-0000-0000-0000-000000000000",
	"firstName": "Matus",
	"lastName": "Zaprazny",
	"email": "matus@zaprazny.com",
    "password": "Matus123!"
}
```

## Book

```json
{
	"id": 123,
	"title": "Alica v krajine zazrakov",
    "description": "Zaujimava poviedka",
    "author": "Lewis Carroll",
    "isbn": "123-456-789-X",
    "totalCopies": 26,
	"createdDateTime": "2024-06-05T15:49:08.433425Z",
	"updatedDateTime": "2024-06-05T15:49:08.4334851Z"
}
```

## Borrowing

```json
{
	"id": 3,
	"userId": "3b3a65fb-749b-4da8-a934-59afb91e4e5b",
	"bookId": 2,
	"isReturned": false,
	"dueDate": "2024-06-06T00:00:00Z",
	"createdDateTime": "2024-06-05T15:51:03.6026316Z"
}
```