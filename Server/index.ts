import app from './src/app';
import connectDB from './src/config/db';
import dotenv from 'dotenv';
import scheduleLotteryDraw from './src/utils/lotteryScheduler';

dotenv.config();
connectDB();

scheduleLotteryDraw();

const PORT = process.env.PORT || 3000;

app.listen(PORT, () => {
  console.log(`Server running on port ${PORT}`);
});
