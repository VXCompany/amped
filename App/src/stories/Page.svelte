<script>
  import './page.css';
  import Header from './Header.svelte';
  import CreateBookmark from './CreateBookmark.svelte';

  let user = null;
</script>

<article>
  <Header {user} on:login={() => user = { name: 'Jane Doe' }} on:logout={() => user = null} on:createAccount={() => user = { name: 'Jane Doe' }} />

  <section>
    <h2>Hello Amped!</h2>
    <CreateBookmark />
    
  </section>
</article>
