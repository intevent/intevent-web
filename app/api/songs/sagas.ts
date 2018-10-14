import { buffers, eventChannel, END } from 'redux-saga';
import { call, put, take, takeEvery } from 'redux-saga/effects';
import { ACTIONS, songsUpdatedAction } from './actions';
import { songsUpdatedObservable } from './graphql';

function handleSubscription() {
  return eventChannel(emitter => {
    const subscription = songsUpdatedObservable().subscribe({
      next: (result) => emitter(result.data.songsUpdated),
      error: (error: Error) => { console.error(error); emitter(END); },
      complete: () => console.log(`complete`),
    });

    return subscription.unsubscribe;
  }, buffers.sliding(2));
}

export function* performBeginSubscription() {
  const channel = yield call(handleSubscription);
  while (true) {
    const songsUpdated = yield take(channel);
    yield put(songsUpdatedAction(songsUpdated));
  }
}

export function* watchBeginSubscription() {
  yield takeEvery(ACTIONS.START_SUBSCRIPTION, performBeginSubscription);
}
