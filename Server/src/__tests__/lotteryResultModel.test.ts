import mongoose from 'mongoose';
import { MongoMemoryServer } from 'mongodb-memory-server';
import LotteryResult from '../api/models/LotteryResult';

describe('LotteryResult Model Test', () => {
  let mongoServer: MongoMemoryServer;

  beforeAll(async () => {
    mongoServer = await MongoMemoryServer.create();
    const uri = mongoServer.getUri();
    await mongoose.connect(uri);
  });

  it('create & save lottery result successfully', async () => {
    const lotteryResultData = { drawTime: new Date(), result: Math.floor(Math.random() * 10) };
    const lotteryResult = new LotteryResult(lotteryResultData);
    const savedLotteryResult = await lotteryResult.save();

    expect(savedLotteryResult._id).toBeDefined();
    expect(savedLotteryResult.result).toBe(lotteryResultData.result);
  });

  afterAll(async () => {
    await mongoose.disconnect();
    await mongoServer.stop();
  });
});
