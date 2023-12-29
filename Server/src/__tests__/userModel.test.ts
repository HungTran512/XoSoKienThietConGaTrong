import mongoose from 'mongoose';
import User from '../api/models/User';
import { MongoMemoryServer } from 'mongodb-memory-server';


describe('User Model Test', () => {
  let mongoServer:MongoMemoryServer;
  beforeAll(async () => {
    mongoServer = await MongoMemoryServer.create();
    const mongoUri = mongoServer.getUri();
    await mongoose.connect(mongoUri);
  });

  it('create & save user successfully', async () => {
    const userData = { name: 'John Doe', DateOfBirth: new Date(), phoneNumber: '1234567891' };
    const validUser = new User(userData);
    const savedUser = await validUser.save();

    expect(savedUser._id).toBeDefined();
    expect(savedUser.name).toBe(userData.name);
    expect(savedUser.phoneNumber).toBe(userData.phoneNumber);
  });


  afterAll(async () => {
    if (mongoServer) {
      await mongoose.disconnect();
      await mongoServer.stop();
    }
  });
});
