import { Request, Response } from 'express';
import LotteryEntry, { ILotteryEntry } from '../models/LotteryEntry';
import LotteryResult from '../models/LotteryResult';


export const createLotteryDraw = async (req: Request, res: Response) => {
  try {
    const user = req.body.User._id;
    const betNumber = req.body.betNumber;
    
    let slotTime = new Date(); // Current time
    slotTime.setMinutes(0, 0, 0);
    slotTime = new Date(slotTime.getTime() + 60 * 60000); // Add one hour
    const newLotteryDraw: ILotteryEntry = new LotteryEntry({
      user,
      slotTime,
      betNumber,
    });
  
    const response = await newLotteryDraw.save();

    const populatedDraw = await LotteryEntry.findById(newLotteryDraw._id).populate('user');

    res.status(201).json(populatedDraw);
  } catch (error:any) {
    res.status(500).json({ message: error.message });
  }
};

export const getLotteryResultsForToday = async (req: Request, res: Response) => {
  try {
    const startOfDay = new Date();
    startOfDay.setHours(0, 0, 0, 0);

    const endOfDay = new Date();
    endOfDay.setHours(23, 59, 59, 999);

    const lotteryDraws = await LotteryResult.find({
      drawTime: {
        $gte: startOfDay,
        $lte: endOfDay
      }
    });

    if (lotteryDraws.length === 0) {
      return res.status(404).json({ message: 'No lottery draws found for today' });
    }

    res.status(200).json(lotteryDraws);
  } catch (error: any) {
    res.status(500).json({ message: error.message });
  }
};

export const getAllLotteryEntries = async (req: Request, res: Response) => {
  try {

    const entries = await LotteryEntry.find().populate('user'); 

    res.status(200).json(entries);
  } catch (error: any) {
    res.status(500).json({ message: error.message });
  }
};