<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-dark px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <img
          alt="logo"
          src="/src/assets/img/Keepr.png"
          height="150"
          width="150"
        />
      </div>
    </router-link>
    <button
      class="navbar-toggler"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon" />
    </button>
    <div class="container-fluid justify-content-center text-success lighten-30">
      <div class="bg-dark text-success title">
        <div class="letter">K</div>
        <div class="letter">E</div>
        <div class="letter">E</div>
        <div class="letter">P</div>
        <div class="letter">R</div>
      </div>
    </div>

    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto"></ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
export default {
  setup() {
    return {};
  },
};
</script>

<style scoped>
a:hover {
  text-decoration: none;
}
.nav-link {
  text-transform: uppercase;
}
.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

/* .letter {
  
  size: 20rem;
  animation-duration: 0.5s;
  animation-name: title;
} */

.title {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  align-items: flex-start;
  justify-content: space-evenly;
}
.letter {
  font-family: shrikhand;
  font-size: xx-large;
  width: 50px;
  height: 50px;

  background-color: var(--bs-dark);
  animation: bounce 1s alternate infinite;

  animation-timing-function: cubic-bezier(0.075, 0.82, 0.165, 1);
}
.letter:nth-child(2) {
  animation-delay: 0.16s;
}
.letter:nth-child(3) {
  animation-delay: 0.32s;
}
.letter:nth-child(4) {
  animation-delay: 0.52s;
}
.letter:nth-child(5) {
  animation-delay: 0.62s;
}
@keyframes bounce {
  from {
    transform: scaleX(1.25);
  }
  to {
    transform: translateY(-50px) scaleX(1);
  }
}
</style>
