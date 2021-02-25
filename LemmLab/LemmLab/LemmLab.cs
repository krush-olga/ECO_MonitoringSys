using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace LemmLab
{
    public class LemmManager
    {
        List<Regex> lemmListEnd;
        /// <summary>
        /// Перетворює стрічку в регулярний вираз та додає умову знаходження в кінці тексту.
        /// </summary>
        /// <param name="end">Стрічка, що перетворюється.</param>
        /// <returns>Регулярний вираз.</returns>
        private Regex RegExEnd(string end)
        {
            return new Regex( end +"$");
        }
		private Regex RegExBegin(string begin)
		{
			return new Regex("^" + begin);
		}
		/// <summary>
		/// Конструктор.
		/// </summary>
		public LemmManager()
        {
            lemmListEnd = new List<Regex>();
            lemmListEnd.Add(RegExEnd("ському"));
            lemmListEnd.Add(RegExEnd("ськими"));
            lemmListEnd.Add(RegExEnd("ської"));
            lemmListEnd.Add(RegExEnd("ською"));
            lemmListEnd.Add(RegExEnd("ським"));
            lemmListEnd.Add(RegExEnd("ська"));
            lemmListEnd.Add(RegExEnd("тись"));
            lemmListEnd.Add(RegExEnd("тися"));
            lemmListEnd.Add(RegExEnd("ого"));
            lemmListEnd.Add(RegExEnd("ити"));
            lemmListEnd.Add(RegExEnd("ись"));
            lemmListEnd.Add(RegExEnd("ою"));
            lemmListEnd.Add(RegExEnd("ам"));
            lemmListEnd.Add(RegExEnd("ом"));
            lemmListEnd.Add(RegExEnd("ами"));
            lemmListEnd.Add(RegExEnd("ах"));
            lemmListEnd.Add(RegExEnd("ої"));
            lemmListEnd.Add(RegExEnd("ії"));
            lemmListEnd.Add(RegExEnd("ія"));
            lemmListEnd.Add(RegExEnd("ій"));
            lemmListEnd.Add(RegExEnd("ти"));
            lemmListEnd.Add(RegExEnd("ть"));
            lemmListEnd.Add(RegExEnd("ів"));
            lemmListEnd.Add(RegExEnd("им"));
            lemmListEnd.Add(RegExEnd("а"));
            lemmListEnd.Add(RegExEnd("и"));
            lemmListEnd.Add(RegExEnd("і"));
            lemmListEnd.Add(RegExEnd("у"));
            lemmListEnd.Add(RegExEnd("о"));
            lemmListEnd.Add(RegExEnd("я"));
            lemmListEnd.Add(RegExEnd("ь"));
            lemmListEnd.Add(RegExEnd("є"));
			lemmListEnd.Add(RegExBegin("пре"));
			lemmListEnd.Add(RegExBegin("все"));
			lemmListEnd.Add(RegExBegin("до"));
			lemmListEnd.Add(RegExBegin("по"));
		}
        /// <summary>
        /// Нормалізує слово.
        /// </summary>
        /// <param name="str">Слово, що потребує нормалізації.</param>
        /// <returns>Нормалізоване слово.</returns>
        public string ToLemm(string str)
        {
            foreach( Regex re in lemmListEnd)
            {
                if (re.IsMatch(str))
                {
                    str = re.Replace(str, "");
                }
            }
            return str;
        }
        /// <summary>
        /// Перетворює стрічку в масив слів.
        /// </summary>
        /// <param name="str">Стрічка, що потребує перетворення.</param>
        /// <returns>Список слів.</returns>
        public string[] ToWords(string str)
        {
            
            Regex re = new Regex("(\\s)|[.,:;\"\"()]");
            return re.Split(str.ToLower());
        }
        /// <summary>
        /// Перетворює зміст html-сторінки в масив слів. Зміст html-тегів при цьому ігнорується.
        /// </summary>
        /// <param name="str">Зміст html-сторінки.</param>
        /// <returns>Масив слів.</returns>
        public string[] ToWordsHTML(string str)                                 //   (\\d)+\\/(?!\\d)|\\/(?!\\d) - 645/97   \\/+ - 645 97
        {
            Regex re1 = new Regex("(\\s)|[.,:;\"()]");
            Regex re2 = new Regex("<(.|\\s|&quot)*?>|[A-Za-z|!|[|=|\\|{|}|&|$|^|…|_|—|«|»|”|“|?|©|%|>+|<+|•]+|\\]|\\/+|\\-(?=\\d)|\\-(?=%)|\\-(?=\\s)|\\-$|\\-{2,5}"); 
            Regex re3 = new Regex("<script language(.|\\s)*?<\\/script>|<div class=\"footer_support col-lg-4 col-md-6 order-md-1\">(.|\\s)*?</div>|<div class=\"cc-block\">(.|\\s)*?</div>|<div class=\"kitsoft-block hidden\">(.|\\s)*?</div>|<ul class=\"column-3-list my-3\">(.|\\s)*?</ul>|<label for=\"link-only\">(.|\\s)*?</div>|<h4 class=\"social-title mb-3\">(.|\\s)*?</h4>|<nobr>(.|\\s)*?</div>|<small class=\"text-muted\">(.|\\s)*?</small>|<div class=\"modal-body\">(.|\\s)*?</div>|<h3 class=\"alert-heading danger alert-no-flexbox\">(.|\\s)*?</noscript>|<ul class=\"navbar-nav mr-auto\">(.|\\s)*?</ul>|<i class=\"icn-rewind\"(.|\\s)*?</a>|<span class=\"d-lg-none\">(.|\\s)*?</div>|<div class=\"checkbox mt-3\">(.|\\s)*?</div>|<div class=\"clearfix\">(.|\\s)*?</a>|target=\"mail_blank\">(.|\\s)*?</div>|<nav class=\"nav nav-separated btn toolbar flex-wrap\">(.|\\s)*?</nav>|<div class=\"mb-3\">(.|\\s)*?</div>|<label for=\"orfo-name\">(.|\\s)*?</label>|<label for=\"orfo-email\">(.|\\s)*?</label>|<div class=\"modal-footer\">(.|\\s)*?</div>|<label for=\"link-code\">(.|\\s)*?</label>|<script>(.|\\s)*?</script>|<div class=\"modal-content\">(.|\\s)*?</div>|<div class=\"nav\">(.|\\s)*?</nav>|<button class=\"btn btn-link mr-3\"(.|\\s)*?</button>|<label for=\"link-code2\">(.|\\s)*?</label>|<div class=\"center-cell\">(.|\\s)*?</div>|<style>(.|\\s)*?<\\/style>");
            Regex re4 = new Regex("'{2}");
           // Regex re5 = new Regex("\\/(?=\\d)");

            return (from a in re1.Split(re2.Replace(re3.Replace(re4.Replace(str.ToLower(), ""), ""), " ")) where a.Trim() != "" select a.Trim()).ToArray();
           // return (from a in re1.Split(re2.Replace(re3.Replace(re4.Replace(re5.Replace(str.ToLower(), "-"), ""), " "), "")) where a.Trim() != "" select a.Trim()).ToArray();

        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     