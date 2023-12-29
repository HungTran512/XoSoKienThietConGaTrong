import { CronJob } from 'cron';
import LotteryResult from '../api/models/LotteryResult';
import moment from 'moment-timezone';

const scheduleLotteryDraw = () => {
  const job = new CronJob('0 * * * *', async function() {

    const timezone = Intl.DateTimeFormat().resolvedOptions().timeZone;
    const drawTime = moment().tz(timezone).toDate();
    const result = Math.floor(Math.random() * 10);
    const lotteryDraw = new LotteryResult({ drawTime, result });
    await lotteryDraw.save();
    console.log(`Lottery Draw: Time - ${drawTime}, Result - ${result}`);
  });

  job.start();
};

export default scheduleLotteryDraw;
