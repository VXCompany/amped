/**
 * @jest-environment jsdom
 */
import '@testing-library/jest-dom'
import {render} from '@testing-library/svelte'
import Comp from '../BookmarkList'

const bookmarks = [
  {
    "uri":"https://totallyamped.com/why-amped-rulez",
    "read":false,
    "owner":"00000000-0000-0000-0000-000000000001"
  },
  {
    "uri":"https://totallyamped.com/because-amped-rulez",
    "read":false,
    "owner":"00000000-0000-0000-0000-000000000001"
  }
];

test('shows list when rendered', () => {
  const comp = render(Comp, {bookmarks: bookmarks});
  const list = comp.getByRole("list");

  expect(list).toBeInTheDocument();
  expect(list).toBeVisible();
  expect(list.children).toHaveLength(2);
})
 