import mongoose from 'mongoose';
import { MongoMemoryServer } from 'mongodb-memory-server';
import LotteryEntry from '../api/models/LotteryEntry';
import User from '../api/models/User';

describe('LotteryEntry Model Test', () => {
let mongoServer: MongoMemoryServer;

  beforeAll(async () => {
    mongoServer = await MongoMemoryServer.create();
    const uri = mongoServer.getUri();
    await mongoose.connect(uri);
  });

  it('create & save lottery entry successfully', async () => {
    const user = new User({ name: 'John Doe', DateOfBirth: new Date(), phoneNumber: '1234567899' });
    const savedUser = await user.save();

    const lotteryEntryData = { user: savedUser._id, betNumber: 5, slotTime: new Date() };
    const lotteryEntry = new LotteryEntry(lotteryEntryData);
    const savedLotteryEntry = await lotteryEntry.save();

    expect(savedLotteryEntry._id).toBeDefined();
    expect(savedLotteryEntry.betNumber).toBe(lotteryEntryData.betNumber);
  });

  afterAll(async () => {
    await mongoose.disconnect();
    await mongoServer.stop();
  });
});
