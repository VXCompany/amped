/**
 * @jest-environment jsdom
 */
import '@testing-library/jest-dom'

import {render} from '@testing-library/svelte'

import Comp from './App'

global.fetch = jest.fn(() =>
  Promise.resolve({
    json: () => Promise.resolve([{"uri":"https://totallyamped.com/why-amped-rulez","read":false,"owner":"00000000-0000-0000-0000-000000000001"}]),
  })
);

beforeEach(() => {
  fetch.mockClear();
});



test('shows proper heading when rendered', () => {
  const {getByText} = render(Comp, {name: 'Amped'})

  expect(getByText('Hello Amped!')).toBeInTheDocument()
})

