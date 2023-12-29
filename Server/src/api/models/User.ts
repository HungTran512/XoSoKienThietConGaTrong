import mongoose, { Document, Schema } from 'mongoose';

export interface IUser extends Document {
  name: string;
  DateOfBirth: Date;
  phoneNumber: string;
}

const UserSchema: Schema = new Schema({
  name: { type: String, required: true },
  DateOfBirth: { type: Date, required: true },
  phoneNumber: { type: String, required: true, unique: true },
});

export default mongoose.model<IUser>('User', UserSchema);
