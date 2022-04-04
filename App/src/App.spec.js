/**
 * @jest-environment jsdom
 */
import '@testing-library/jest-dom'

import {render} from '@testing-library/svelte'

import Comp from './App'

test('shows proper heading when rendered', () => {
  const {getByText} = render(Comp, {name: 'Amped'})

  expect(getByText('Hello Amped!')).toBeInTheDocument()
})

