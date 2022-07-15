import CreateBookmark from './CreateBookmark.svelte';

export default {
  title: 'Amped/Bookmark',
  component: CreateBookmark,
  parameters: {
    // More on Story layout: https://storybook.js.org/docs/svelte/configure/story-layout
    layout: 'fullscreen',
  },
  argTypes: {
    onCreateBookmark: { action: 'onCreateBookmark' }
  },
};

const Template = (args) => ({
  Component: CreateBookmark,
  props: args,
  on: {
    createBookmark: args.onCreateBookmark
  },
});

export const Create = Template.bind({});
CreateBookmark.args = {};
