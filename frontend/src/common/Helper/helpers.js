function validateEmail(email) {
  const re =
    /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  return re.test(email);
}

function validateTel(telefone) {
  telefone = telefone.replace(/\D/g, "");

  if (!(telefone.length >= 10 && telefone.length <= 11)) return false;

  if (telefone.length == 11 && parseInt(telefone.substring(2, 3)) != 9)
    return false;

  for (var n = 0; n < 10; n++) {
    if (telefone == new Array(11).join(n) || telefone == new Array(12).join(n))
      return false;
  }

  var codigosDDD = [
    11, 12, 13, 14, 15, 16, 17, 18, 19, 21, 22, 24, 27, 28, 31, 32, 33, 34, 35,
    37, 38, 41, 42, 43, 44, 45, 46, 47, 48, 49, 51, 53, 54, 55, 61, 62, 64, 63,
    65, 66, 67, 68, 69, 71, 73, 74, 75, 77, 79, 81, 82, 83, 84, 85, 86, 87, 88,
    89, 91, 92, 93, 94, 95, 96, 97, 98, 99,
  ];

  if (codigosDDD.indexOf(parseInt(telefone.substring(0, 2))) == -1)
    return false;

  if (new Date().getFullYear() < 2017) return true;
  if (
    telefone.length == 10 &&
    [2, 3, 4, 5, 7].indexOf(parseInt(telefone.substring(2, 3))) == -1
  )
    return false;

  return true;
}
function validateCPF(cpf) {
  const notDig = (i) => ![".", "-", " "].includes(i);
  const prepare = (cpf) => cpf.trim().split("").filter(notDig).map(Number);
  const is11Len = (cpf) => cpf.length === 11;
  const notAllEquals = (cpf) => !cpf.every((i) => cpf[0] === i);
  const onlyNum = (cpf) => cpf.every((i) => !isNaN(i));
  const calcDig = (limit) => (a, i, idx) => a + i * (limit + 1 - idx);
  const somaDig = (cpf, limit) => cpf.slice(0, limit).reduce(calcDig(limit), 0);
  const resto11 = (somaDig) => 11 - (somaDig % 11);
  const zero1011 = (resto11) => ([10, 11].includes(resto11) ? 0 : resto11);
  const getDV = (cpf, limit) => zero1011(resto11(somaDig(cpf, limit)));
  const verDig = (pos) => (cpf) => getDV(cpf, pos) === cpf[pos];

  const checks = [is11Len, notAllEquals, onlyNum, verDig(9), verDig(10)];

  return checks.map((f) => f(prepare(cpf))).every((r) => !!r);
}
export default {
  validateCPF,
  validateTel,
  validateEmail,
};
