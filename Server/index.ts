import app from "./src/app";
import connectDB from "./src/config/db";
import dotenv from "dotenv";
import scheduleLotteryDraw from "./src/utils/lotteryScheduler";
import scheduleDailyUserCount from "./src/utils/userCountScheduler";

dotenv.config();
connectDB();

scheduleLotteryDraw();
scheduleDailyUserCount();

const PORT = process.env.PORT || 3000;

app.listen(PORT, () => {
  console.log(`Server running on port ${PORT}`);
});
