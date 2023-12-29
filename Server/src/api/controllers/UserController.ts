import { Request, Response } from 'express';
import User, { IUser } from '../models/User';

export const createUser = async (req: Request, res: Response) => {
  try {
    const { name,  DateOfBirth, phoneNumber } = req.body;

    const newUser: IUser = new User({
      name,
      DateOfBirth,
      phoneNumber,
    });

    const response = await newUser.save();

    res.status(201).json(newUser);
  } catch (error:any) {
    res.status(500).json({ message: error.message });
  }
};

export const getUser = async (req: Request, res: Response) => {
  try {
    const { phoneNumber } = req.params;
    const user = await User.findOne({ phoneNumber });
    if (!user) {
      return res.status(404).json({ message: 'User not found' });
    }
  
    res.status(200).json(user);
  } catch (error:any) {
    res.status(500).json({ message: error.message });
  }
};
