using System.Threading.Tasks;

namespace LearnProject
{
    public interface ILessonFragment
    {
        public Task PlayFragment(LessonFragmentSO fragment);
    }
}
