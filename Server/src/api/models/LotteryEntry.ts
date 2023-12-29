import mongoose, { Document, Schema } from 'mongoose';
import { IUser } from './User';

export interface ILotteryEntry extends Document {
  user: IUser['_id'];
  betNumber: number;
  slotTime: Date;
}

const LotteryEntrySchema: Schema = new Schema({
  user: { type: mongoose.Schema.Types.ObjectId, ref: 'User', required: true },
  betNumber: { type: Number, required: true },
  slotTime: { type: Date, required: true }
});

export default mongoose.model<ILotteryEntry>('LotteryEntry', LotteryEntrySchema);
