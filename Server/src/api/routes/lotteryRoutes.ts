import express from 'express';
import * as LotteryController from '../controllers/LotteryController';

const router = express.Router();


router.post('/entry', LotteryController.createLotteryDraw);

router.get('/results/today', LotteryController.getLotteryResultsForToday);

router.get('/entries/all', LotteryController.getAllLotteryEntries);

export default router;
