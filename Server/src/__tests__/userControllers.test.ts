import request from 'supertest';
import mongoose from 'mongoose';
import app from '../app'; 
import User from '../api/models/User';
import { MongoMemoryServer } from 'mongodb-memory-server';



describe('User Controller Test', () => {
  let mongoServer:MongoMemoryServer;
  beforeAll(async () => {
    mongoServer = await MongoMemoryServer.create();
    const mongoUri = mongoServer.getUri();
    await mongoose.connect(mongoUri);
  });

  it('should create a new user', async () => {
    const res = await request(app)
      .post('/user/register')
      .send({
        name: 'Jane Doe',
        DateOfBirth: '1990-01-01',
        phoneNumber: '0987654321'
      });

    expect(res.status).toEqual(201);
    expect(res.body).toHaveProperty('_id');
    expect(res.body.name).toBe('Jane Doe');
  });

  it('should retrieve a user by phone number', async () => {
    const user = new User({ name: 'John Doe', DateOfBirth: new Date(), phoneNumber: '1234567890' });
    await user.save();

    const res = await request(app)
      .get(`/user/${user.phoneNumber}`);

    expect(res.status).toEqual(200);
    expect(res.body.name).toBe('John Doe');
  });

  afterAll(async () => {
    if (mongoServer) {
      await mongoose.disconnect();
      await mongoServer.stop();
    }
  });
});
