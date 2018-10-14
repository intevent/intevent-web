import { buffers, eventChannel, END } from 'redux-saga';
import { call, put, take, takeEvery } from 'redux-saga/effects';
import { ACTIONS, votingResultsUpdatedAction } from './actions';
import { votingResultsUpdatedObservable } from './graphql';

function handleSubscription() {
  return eventChannel(emitter => {
    const subscription = votingResultsUpdatedObservable().subscribe({
      next: (result) => emitter(result.data.votingResultsUpdated),
      error: (error: Error) => { console.error(error); emitter(END); },
      complete: () => console.log(`complete`),
    });

    return subscription.unsubscribe;
  }, buffers.sliding(2));
}

export function* performBeginSubscription() {
  const channel = yield call(handleSubscription);
  while (true) {
    const votingResultsUpdated = yield take(channel);
    yield put(votingResultsUpdatedAction(votingResultsUpdated));
  }
}

export function* watchBeginSubscription() {
  yield takeEvery(ACTIONS.START_SUBSCRIPTION, performBeginSubscription);
}
