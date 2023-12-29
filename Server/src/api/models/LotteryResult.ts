import mongoose, { Document, Schema } from 'mongoose';


export interface ILotteryResult extends Document {
  drawTime: Date;
  result: number;

}

const LotteryResultSchema: Schema = new Schema({
  drawTime: { type: Date, required: true },
  result: { type: Number, required: true },
});

export default mongoose.model<ILotteryResult>('LotteryResult', LotteryResultSchema);
