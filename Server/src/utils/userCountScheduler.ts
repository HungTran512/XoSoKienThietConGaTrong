import { CronJob } from "cron";
import User from "../api/models/User";
import moment from "moment-timezone";

const scheduleDailyUserCount = (): void => {
  const job = new CronJob("0 0 * * *", async () => {
    const timezone = Intl.DateTimeFormat().resolvedOptions().timeZone;
    const currentTime = moment().tz(timezone).toDate();

    try {
      const userCount = await User.countDocuments();
      console.log(`User count as of ${currentTime}: ${userCount}`);
    } catch (error) {
      if (error instanceof Error) {
        console.error("Error counting users:", error.message);
      } else {
        console.error("An unknown error occurred");
      }
    }
  });

  job.start();
};

export default scheduleDailyUserCount;
