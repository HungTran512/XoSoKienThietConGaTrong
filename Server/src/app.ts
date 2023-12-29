import express from 'express';
import userRoutes from './api/routes/userRoutes';
import lotteryRoutes from './api/routes/lotteryRoutes';

const app = express();

app.use(express.json());

app.use('/user', userRoutes);

app.use('/lottery', lotteryRoutes);

// Error handler middleware
app.use((err: any, req: express.Request, res: express.Response, next: express.NextFunction) => {
  console.error(err.stack);
  res.status(500).send('Something broke!');
});

export default app;
