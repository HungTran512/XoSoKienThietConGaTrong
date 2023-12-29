# Xổ Số Kiến Thiết Con Gà Trống (Back-End)

## Description

This application is the back-end for the lottery system named "Xổ Số Kiến Thiết Con Gà Trống". It allows users to register and place bets on a randomly generated number between 0 and 9, which occurs at the start of every hour.

## Structure

- `api/models/`: Contains Mongoose models for Users and Lottery Draws.
- `api/controllers/`: Contains the business logic for user registration and lottery draws.
- `api/routes/`: Defines the API endpoints.
- `util/lotteryScheduler.ts`: Sets up a cron job for the lottery draw.
- `util/userCounterScheduler.ts`: Sets up a cron job for measuring users.
- `tests/`: Includes basic unit tests.
- `index.ts`: The entry point for the application.

## Prerequisites

- Node.js
- MongoDB
- NPM

## Setup

1. Clone the repository.
2. Install dependencies:
3. Set up environment variables in a `.env` file, and example can be:

```
DB_URI= "mongodb://127.0.0.1:27017/lottery"
PORT = 3000
```

## Running the Application

1. Start MongoDB in your local environment.
2. To run the server, execute:

```
npm run dev
```

3. The server should start and the cron job will schedule lottery draws every hour.

## Running Tests

To run the unit tests, execute:

```
 npm run test
```

## API Endpoints

- POST `/user/register`: Registers a new user.
- GET `/user/:phoneNumber`: retrieve user's information by phone number
- POST `/lottery/entry`: create a lottery entry for the upcoming slot.
- GET `/lottery/results/today`: retrieve lotery result of that day (assuming that user only need that information).
- GET `/lottery/entries/all`: retrieve all lottery entries.

## Notes

- Ensure MongoDB is running before starting the application.
- The cron job is set to run every hour, simulating a lottery draw.

## Author

Hung Tran

## License

This project is licensed under the ISC License.
