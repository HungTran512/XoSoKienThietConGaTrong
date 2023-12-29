import request from 'supertest';
import mongoose from 'mongoose';
import { MongoMemoryServer } from 'mongodb-memory-server';
import app from '../app'; 
import User from '../api/models/User';

describe('LotteryEntry Controller Test', () => {
  let mongoServer: MongoMemoryServer;

  beforeAll(async () => {
    mongoServer = await MongoMemoryServer.create();
    const uri = mongoServer.getUri();
    await mongoose.connect(uri);
  });

  it('should create a new lottery draw', async () => {
    const user = new User({ name: 'Jane D', DateOfBirth: new Date(), phoneNumber: '0987654322' });
    const savedUser = await user.save();

    const res = await request(app)
      .post('/lottery/entry')
      .send({
        User: savedUser,
        betNumber: 3,
      });

    expect(res.status).toEqual(201);
    expect(res.body).toHaveProperty('_id');
    expect(res.body.betNumber).toBe(3);
  });

  afterAll(async () => {
    await mongoose.disconnect();
    await mongoServer.stop();
  });
});
