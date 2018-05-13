import 'tslib';
import { Action, ActionCreator } from 'redux';

export enum ACTIONS {
  SOME_ACTION = 'home/some-action',
};

export interface SomeAction extends Action {
  type: ACTIONS.SOME_ACTION,
};

export const someAction: ActionCreator<SomeAction> = () => ({
  type: ACTIONS.SOME_ACTION,
});
