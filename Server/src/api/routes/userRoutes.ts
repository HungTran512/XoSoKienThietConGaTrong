import express from 'express';
import * as userController from '../controllers/UserController';

const router = express.Router();

// Route to create a new user
router.post('/register', userController.createUser);

// Route to get a user by phone number
router.get('/:phoneNumber', userController.getUser);

export default router;
