import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

export interface ResultsState {
    isLoading: boolean;
    results: Results;
}

export interface Results {
    succeeded: number;
    failed: number;
}

interface UploadAction {
    type: 'UPLOAD';
}

interface ResultsAction {
    type: 'RESULTS';
    results: Results;
}

type KnownAction = UploadAction | ResultsAction;

export const actionCreators = {
    requestUpload: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
    }
};

const unloadedState: ResultsState = {
    isLoading: false, results: { failed: 0, succeeded: 0} };

export const reducer: Reducer<ResultsState> = (state: ResultsState | undefined, incomingAction: Action): ResultsState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'UPLOAD':
        return {
            isLoading: true, results: { failed: 0, succeeded: 0 }
        };
        case 'RESULTS':
        return {
            isLoading: false, results: { failed: 0, succeeded: 0 }
        };

        break;
    }

    return state;
};
