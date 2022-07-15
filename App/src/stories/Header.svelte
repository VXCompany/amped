<script>
  import './header.css';
  import Button from './Button.svelte';

  import { createEventDispatcher } from 'svelte';

  export let user = null;

  const dispatch = createEventDispatcher();

  function onLogin(event) {
    dispatch('login', event);
  }
  function onLogout(event) {
    dispatch('logout', event);
  }
  function onCreateAccount(event) {
    dispatch('createAccount', event);
  }
</script>

<header>
  <div class="wrapper">
    <div>

      <svg version="1.0" xmlns="http://www.w3.org/2000/svg"
      width="32.000000pt" height="32.000000pt" viewBox="0 0 32.000000 32.000000"
      preserveAspectRatio="xMidYMid meet">
     
     <g transform="translate(0.000000,32.000000) scale(0.100000,-0.100000)"
     fill="#5E44CA" stroke="none">
     <path d="M74 304 c-48 -11 -59 -39 -59 -145 0 -89 2 -101 23 -121 20 -21 32
     -23 122 -23 137 0 145 8 145 145 0 132 -9 143 -127 146 -46 1 -93 0 -104 -2z
     m56 -85 c0 -14 7 -19 30 -19 26 0 30 4 30 26 0 14 4 23 10 19 6 -3 10 -15 10
     -26 0 -10 5 -19 10 -19 6 0 10 -13 10 -30 0 -20 -5 -30 -15 -30 -8 0 -17 -11
     -21 -25 -8 -34 -46 -34 -64 0 -7 14 -19 25 -27 25 -8 0 -13 12 -13 30 0 17 5
     30 10 30 6 0 10 12 10 26 0 14 4 23 10 19 6 -3 10 -15 10 -26z"/>
     </g>
     </svg>

      <h1>Amped</h1>
    </div>
    <div>
      {#if user}
        <span class="welcome">
          Welcome, <b>{user.name}</b>!
        </span>
        <Button size="small" on:click={onLogout} label="Log out" />
      {/if}
      {#if !user}
        <Button size="small" on:click={onLogin} label="Log in" />
        <Button primary size="small" on:click={onCreateAccount} label="Sign up" />
      {/if}
    </div>
  </div>
</header>
