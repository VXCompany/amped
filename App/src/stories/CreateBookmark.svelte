<script>
  import './createbookmark.css';
  import Button from './Button.svelte';
  import { createEventDispatcher } from 'svelte';

  const dispatch = createEventDispatcher();

  function onCreateBookmark(event) {
    dispatch('createBookmark', event);
  }

</script>

<form aria-label="form">
  <div class="wrapper">
      <div class="container">
          <input type="input" placeholder="Url" />
          <Button primary size="large" on:click={onCreateBookmark} label="Add bookmark" />
      </div>
  </div>
</form>

