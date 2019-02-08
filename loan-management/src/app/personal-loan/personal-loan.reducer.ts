export function reducer(state, action) {
    switch (action.type) {
        case 'TOGGLE_TOPUP':
            console.log('existing state: ' + JSON.stringify(state));
            console.log('payload: ' + action.payload)
            return {
                ...state,
                accountNo: action.payload
            }
        default:
            console.log('default state');
            return state;
    }
}