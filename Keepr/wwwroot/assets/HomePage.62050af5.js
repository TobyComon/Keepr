import{G as c,g as n,h as p,o as s,k as a,j as _,F as d,m as i,s as l}from"./vendor.1d610a41.js";import{_ as m,A as u,k as f,l as v,P as k}from"./index.c4ff0b93.js";const g={setup(){return c(async()=>{try{await f.getAll()}catch(e){v.error(e),k.toast(e.message,"error")}}),{keeps:n(()=>u.keeps)}}},h={class:"container"},y={class:"masonry-with-columns"};function x(e,P,j,r,w,A){const o=p("Keep");return s(),a("div",h,[_("div",y,[(s(!0),a(d,null,i(r.keeps,t=>(s(),a("div",{key:t.id},[l(o,{keep:t},null,8,["keep"])]))),128))])])}var E=m(g,[["render",x],["__scopeId","data-v-c4a3f2ca"]]);export{E as default};